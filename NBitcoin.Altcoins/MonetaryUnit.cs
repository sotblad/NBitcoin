using NBitcoin;
using NBitcoin.DataEncoders;
using NBitcoin.Protocol;
using NBitcoin.RPC;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace NBitcoin.Altcoins
{
	// Reference: https://github.com/muecoin/MUE/blob/master/src/chainparams.cpp
	public class MonetaryUnit : NetworkSetBase
	{
		public static MonetaryUnit Instance { get; } = new MonetaryUnit();

		public override string CryptoCode => "MUE";

		private MonetaryUnit()
		{

		}
#pragma warning disable CS0618 // Type or member is obsolete
		public class MonetaryUnitConsensusFactory : ConsensusFactory
		{
			private MonetaryUnitConsensusFactory()
			{
			}
			
			public static MonetaryUnitConsensusFactory Instance { get; } = new MonetaryUnitConsensusFactory();
			
			public override BlockHeader CreateBlockHeader()
			{
				return new MonetaryUnitBlockHeader();
			}
			public override Block CreateBlock()
			{
				return new MonetaryUnitBlock(new MonetaryUnitBlockHeader());
			}
		}
		
		public class MonetaryUnitBlockHeader : BlockHeader
		{
			public override uint256 GetPoWHash()
			{
				throw new NotSupportedException("PoW for MonetaryUnit MUE is not supported");
			}
		}

		public class MonetaryUnitBlock : Block
		{
			public MonetaryUnitBlock(MonetaryUnitBlockHeader header) : base(header)
			{

			}

			public override ConsensusFactory GetConsensusFactory()
			{
				return MonetaryUnitConsensusFactory.Instance;
			}
		}
#pragma warning restore CS0618 // Type or member is obsolete

		//Format visual studio
		//{({.*?}), (.*?)}
		//Tuple.Create(new byte[]$1, $2)
		//static Tuple<byte[], int>[] pnSeed6_main = null;
		//static Tuple<byte[], int>[] pnSeed6_test = null;

		protected override NetworkBuilder CreateMainnet()
		{
			var builder = new NetworkBuilder();
			builder.SetConsensus(new Consensus()
			{
				SubsidyHalvingInterval = 210000,
				MajorityEnforceBlockUpgrade = 750,
				MajorityRejectBlockOutdated = 950,
				MajorityWindow = 1000,
				PowLimit = new Target(0 >> 1),
				PowTargetTimespan = TimeSpan.FromSeconds(10 * 40),
				PowTargetSpacing = TimeSpan.FromSeconds(1 * 40),
				PowAllowMinDifficultyBlocks = false,
				CoinbaseMaturity = 50,
				PowNoRetargeting = false,
				ConsensusFactory = MonetaryUnitConsensusFactory.Instance,
				LitecoinWorkCalculation = false,
				SupportSegwit = false
			})
			.SetBase58Bytes(Base58Type.PUBKEY_ADDRESS, new byte[] { 16 })
			.SetBase58Bytes(Base58Type.SCRIPT_ADDRESS, new byte[] { 76 })
			.SetBase58Bytes(Base58Type.SECRET_KEY, new byte[] { 126 })
			.SetBase58Bytes(Base58Type.EXT_PUBLIC_KEY, new byte[] { 0x02, 0x2D, 0x25, 0x33 })
			.SetBase58Bytes(Base58Type.EXT_SECRET_KEY, new byte[] { 0x02, 0x21, 0x31, 0x2B })
			.SetBech32(Bech32Type.WITNESS_PUBKEY_ADDRESS, Encoders.Bech32("mue"))
			.SetBech32(Bech32Type.WITNESS_SCRIPT_ADDRESS, Encoders.Bech32("mue"))
			.SetMagic(0xeafdc491)
			.SetPort(19687)
			.SetRPCPort(19688)
			.SetMaxP2PVersion(70800)
			.SetName("monetaryunit-main")
			.AddAlias("monetaryunit-mainnet")
			.AddDNSSeeds(new[]{
				new DNSSeedData("92.60.45.220", "92.60.45.220"),
			})
			.AddSeeds(new NetworkAddress[0])
			.SetGenesis("0100000000000000000000000000000000000000000000000000000000000000000000006db905142382324db417761891f2d2f355ea92f27ab0fc35e59e90b50e0534edf5d2af59ffff001ff9787a00e965ffd002cd6ad0e2dc402b8044de833e06b23127ea8c3d80aec9141077149556e81f171bcc55a6ff8345e692c0f86e5b48e01b996cadc001622fb5e363b4210000000000000000000000000000000000000000000000000000000000000000ffffffff000101000000010000000000000000000000000000000000000000000000000000000000000000ffffffff420004bf91221d0104395365702030322c203230313720426974636f696e20627265616b732024352c30303020696e206c6174657374207072696365206672656e7a79ffffffff0100f2052a010000004341040d61d8653448c98731ee5fffd303c15e71ec2057b77f11ab3601979728cdaff2d68afbba14e4fa0bc44f2072b0b23ef63717f8cdfbe58dcd33f32b6afe98741aac00000000");
			return builder;
		}

		protected override NetworkBuilder CreateTestnet()
		{
			var builder = new NetworkBuilder();
			builder.SetConsensus(new Consensus()
			{
				SubsidyHalvingInterval = 210000,
				MajorityEnforceBlockUpgrade = 51,
				MajorityRejectBlockOutdated = 75,
				MajorityWindow = 100,
				PowLimit = new Target(0 >> 1),
				PowTargetTimespan = TimeSpan.FromSeconds(1 * 60),
				PowTargetSpacing = TimeSpan.FromSeconds(1 * 10),
				PowAllowMinDifficultyBlocks = false,
				CoinbaseMaturity = 50,
				PowNoRetargeting = false,
				ConsensusFactory = MonetaryUnitConsensusFactory.Instance,
				LitecoinWorkCalculation = false,
				SupportSegwit = false
			})
			.SetBase58Bytes(Base58Type.PUBKEY_ADDRESS, new byte[] { 139 })
			.SetBase58Bytes(Base58Type.SCRIPT_ADDRESS, new byte[] { 19 })
			.SetBase58Bytes(Base58Type.SECRET_KEY, new byte[] { 239 })
			.SetBase58Bytes(Base58Type.EXT_PUBLIC_KEY, new byte[] { 0x3A, 0x80, 0x61, 0xA0 })
			.SetBase58Bytes(Base58Type.EXT_SECRET_KEY, new byte[] { 0x3A, 0x80, 0x58, 0x37 })
			.SetBech32(Bech32Type.WITNESS_PUBKEY_ADDRESS, Encoders.Bech32("tmue"))
			.SetBech32(Bech32Type.WITNESS_SCRIPT_ADDRESS, Encoders.Bech32("tmue"))
			.SetMagic(0xbd657647)
			.SetPort(19685)
			.SetRPCPort(19686)
			.SetMaxP2PVersion(70800)
			.SetName("monetaryunit-test")
			.AddAlias("monetaryunit-testnet")
			.AddDNSSeeds(new[]{
				new DNSSeedData("92.60.45.220", "92.60.45.220"),
			})
			.AddSeeds(new NetworkAddress[0])
			// Incorrect, using mainnet for now
			.SetGenesis("0100000000000000000000000000000000000000000000000000000000000000000000006db905142382324db417761891f2d2f355ea92f27ab0fc35e59e90b50e0534edf5d2af59ffff001ff9787a00e965ffd002cd6ad0e2dc402b8044de833e06b23127ea8c3d80aec9141077149556e81f171bcc55a6ff8345e692c0f86e5b48e01b996cadc001622fb5e363b4210000000000000000000000000000000000000000000000000000000000000000ffffffff000101000000010000000000000000000000000000000000000000000000000000000000000000ffffffff420004bf91221d0104395365702030322c203230313720426974636f696e20627265616b732024352c30303020696e206c6174657374207072696365206672656e7a79ffffffff0100f2052a010000004341040d61d8653448c98731ee5fffd303c15e71ec2057b77f11ab3601979728cdaff2d68afbba14e4fa0bc44f2072b0b23ef63717f8cdfbe58dcd33f32b6afe98741aac00000000");
			return builder;
		}

		protected override NetworkBuilder CreateRegtest()
		{
			var builder = new NetworkBuilder();
			builder.SetConsensus(new Consensus()
			{
				SubsidyHalvingInterval = 150,
				MajorityEnforceBlockUpgrade = 750,
				MajorityRejectBlockOutdated = 950,
				MajorityWindow = 1000,
				PowLimit = new Target(0 >> 1),
				PowTargetTimespan = TimeSpan.FromSeconds(24 * 60 * 60),
				PowTargetSpacing = TimeSpan.FromSeconds(1 * 60),
				PowAllowMinDifficultyBlocks = true,
				CoinbaseMaturity = 50,
				PowNoRetargeting = false,
				ConsensusFactory = MonetaryUnitConsensusFactory.Instance,
				LitecoinWorkCalculation = false,
				SupportSegwit = false
			})
			.SetBase58Bytes(Base58Type.PUBKEY_ADDRESS, new byte[] { 139 })
			.SetBase58Bytes(Base58Type.SCRIPT_ADDRESS, new byte[] { 19 })
			.SetBase58Bytes(Base58Type.SECRET_KEY, new byte[] { 239 })
			.SetBase58Bytes(Base58Type.EXT_PUBLIC_KEY, new byte[] { 0x3A, 0x80, 0x61, 0xA0 })
			.SetBase58Bytes(Base58Type.EXT_SECRET_KEY, new byte[] { 0x3A, 0x80, 0x58, 0x37 })
			.SetBech32(Bech32Type.WITNESS_PUBKEY_ADDRESS, Encoders.Bech32("rtmue"))
			.SetBech32(Bech32Type.WITNESS_SCRIPT_ADDRESS, Encoders.Bech32("rtmue"))
			.SetMagic(0xad7ecfa2)
			.SetPort(19685)
			.SetRPCPort(19686)
			.SetMaxP2PVersion(70800)
			.SetName("monetaryunit-reg")
			.AddAlias("monetaryunit-regtest")
			.AddDNSSeeds(new DNSSeedData[0])
			.AddSeeds(new NetworkAddress[0])
			// Incorrect, using mainnet for now
			.SetGenesis("0100000000000000000000000000000000000000000000000000000000000000000000006db905142382324db417761891f2d2f355ea92f27ab0fc35e59e90b50e0534edf5d2af59ffff7f2011000000e965ffd002cd6ad0e2dc402b8044de833e06b23127ea8c3d80aec9141077149556e81f171bcc55a6ff8345e692c0f86e5b48e01b996cadc001622fb5e363b4210000000000000000000000000000000000000000000000000000000000000000ffffffff000101000000010000000000000000000000000000000000000000000000000000000000000000ffffffff420004bf91221d0104395365702030322c203230313720426974636f696e20627265616b732024352c30303020696e206c6174657374207072696365206672656e7a79ffffffff0100f2052a010000004341040d61d8653448c98731ee5fffd303c15e71ec2057b77f11ab3601979728cdaff2d68afbba14e4fa0bc44f2072b0b23ef63717f8cdfbe58dcd33f32b6afe98741aac00000000");
			return builder;
		}

		protected override void PostInit()
		{
			RegisterDefaultCookiePath("MonetaryUnit");
		}

	}
}
